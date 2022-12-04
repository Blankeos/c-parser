using System;
using System.IO;
// using System.Collection;

namespace CParser
{

    class Node
    {
        public string type;

        public Node(string type)
        {
            this.type = type;
        }
        public override string ToString()
        {
            return $"<{type}>";
        }
    }

    class FuncDecNode : Node
    {
        public DataTypeNode dataType;
        public IdentifierNode identifier;

        public FuncDecNode(DataTypeNode dataTypeNode, IdentifierNode identifierNode) : base("FuncDecNode")
        {
            this.dataType = dataTypeNode;
            this.identifier = identifierNode;
        }
    }

    class DataTypeNode : Node
    {
        public string value;

        public DataTypeNode(string value) : base("DataTypeNode")
        {
            this.value = value;
        }
    }

    class IdentifierNode : Node
    {
        public string value;

        public IdentifierNode(string value) : base("IdentifierNode")
        {
            this.value = value;
        }
    }
}