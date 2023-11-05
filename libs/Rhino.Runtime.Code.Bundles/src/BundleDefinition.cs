using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

using Y = YamlDotNet.RepresentationModel;

namespace Rhino.Runtime.Code.Bundles
{
    public sealed class BundleDefinition
    {
        public static BundleDefinition Empty { get; } = new BundleDefinition();

        public static bool TryReadData(BundleVisitor visitor, string path, out BundleDefinition data)
        {
            data = default;

            try
            {
                using (var reader = new StreamReader(path))
                {
                    var yml = new Y.YamlStream();
                    yml.Load(reader);

                    Y.YamlNode root = yml.Documents[0].RootNode;

                    data = new BundleDefinition(path, (Y.YamlMappingNode)root);
                    return true;
                }
            }
            catch (Exception ex)
            {
                visitor.Logger.Error(ex.Message);
            }

            return false;
        }

        readonly Y.YamlMappingNode _root = new Y.YamlMappingNode();

        public FileInfo FileInfo { get; }

        BundleDefinition() { }

        BundleDefinition(string path, Y.YamlMappingNode root)
        {
            _root = root;

            FileInfo = new FileInfo(path);
        }

        public string Get(string key) => ((Y.YamlScalarNode)_root[new Y.YamlScalarNode(key)]).Value;

        public bool TryGet(string key, out string value)
        {
            value = default;

            if (_root.FirstOrDefault(n => n.Key is Y.YamlScalarNode k
                                          && k.Value is string kk
                                          && kk == key)
                            is KeyValuePair<Y.YamlNode, Y.YamlNode> pair
                      && pair.Value is Y.YamlScalarNode v)
            {
                value = v.Value;
                return true;
            }

            return false;
        }

        public IEnumerable<string> GetEnumerable(string key)
        {
            Y.YamlSequenceNode value = (Y.YamlSequenceNode)_root[new Y.YamlScalarNode(key)];
            foreach (Y.YamlScalarNode node in value.OfType<Y.YamlScalarNode>())
                yield return node.Value;
        }

        public bool TryGetEnumerable(string key, out IEnumerable<string> values)
        {
            values = default;

            if (_root.FirstOrDefault(n => n.Key is Y.YamlScalarNode k && k.Value == key)
                            is KeyValuePair<Y.YamlNode, Y.YamlNode> pair
                      && pair.Value is Y.YamlSequenceNode v)
            {
                values = v.OfType<Y.YamlScalarNode>().Select(vv => vv.Value);
                return true;
            }

            return false;
        }

        public IDictionary<string, string> GetDictionary(string key)
        {
            Y.YamlMappingNode value  = (Y.YamlMappingNode)_root[new Y.YamlScalarNode(key)];
            return value.ToDictionary(p => ((Y.YamlScalarNode)p.Key).Value, p => ((Y.YamlScalarNode)p.Value).Value);
        }

        public bool TryGetDictionary(string key, out IDictionary<string, string> values)
        {
            values = default;

            if (_root.FirstOrDefault(n => n.Key is Y.YamlScalarNode k && k.Value == key)
                            is KeyValuePair<Y.YamlNode, Y.YamlNode> pair
                      && pair.Value is Y.YamlMappingNode v)
            {
                values = v.ToDictionary(p => ((Y.YamlScalarNode)p.Key).Value, p => ((Y.YamlScalarNode)p.Value).Value);
                return true;
            }

            return false;
        }
    }
}
