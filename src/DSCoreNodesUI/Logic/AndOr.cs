﻿using System.Collections.Generic;
using DSCoreNodesUI;
using Dynamo.Models;
using ProtoCore.AST.AssociativeAST;
using ProtoCore.DSASM;

namespace DSCoreNodes.Logic
{
    //TODO: Make Variable Input?
    /// <summary>
    /// Abstract base class for short-circuiting binary logic operators.
    /// </summary>
    public abstract class BinaryLogic : VariableInputNode
    {
        private readonly Operator _op;

        protected BinaryLogic(string symbol, Operator op)
        {
            _op = op;

            InPortData.Add(new PortData("a", "operand"));
            InPortData.Add(new PortData("b", "operand"));
            OutPortData.Add(new PortData("a" + symbol + "b", "result"));
            RegisterAllPorts();
        }

        public override IEnumerable<AssociativeNode> BuildOutputAst(List<AssociativeNode> inputAstNodes)
        {
            return new[]
            {
                AstFactory.BuildAssignment(
                    GetAstIdentifierForOutputIndex(0),
                    AstFactory.BuildBinaryExpression(inputAstNodes[0], inputAstNodes[1], _op))
            };
        }

        protected override string InputRootName
        {
            get { return "bool"; }
        }

        protected override string TooltipRootName
        {
            get { return "Boolean #"; }
        }
    }

    /// <summary>
    /// Short-circuiting Logical AND
    /// </summary>
    public class And : BinaryLogic
    {
        public And() : base("∧", Operator.and) { }
    }

    /// <summary>
    /// Short-circuiting Logical OR
    /// </summary>
    public class Or : BinaryLogic
    {
        public Or() : base("∨", Operator.or) { }
    }
}