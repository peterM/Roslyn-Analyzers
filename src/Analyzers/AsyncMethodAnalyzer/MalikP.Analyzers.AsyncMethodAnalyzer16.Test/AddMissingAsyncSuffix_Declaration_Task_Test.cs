// MIT License
//
// Copyright (c) 2019 Peter Malik.
// 
// File: AsyncMethodAnalyzerUnitTests.cs 
// Company: MalikP.
//
// Repository: https://github.com/peterM/Roslyn-Analyzers
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System;
using MalikP.Analyzers.AsyncMethodAnalyzer.Analyzers.Specific;
using MalikP.Analyzers.AsyncMethodAnalyzer.Analyzers.Specific.Naming;
using MalikP.Analyzers.AsyncMethodAnalyzer.CodeFixes.Specific;
using MalikP.Analyzers.AsyncMethodAnalyzer.Rules.Naming;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using TestHelper;

namespace MalikP.Analyzers.AsyncMethodAnalyzer.Test
{
    [TestClass]
    public class AddMissingAsyncSuffix_Declaration_Task_Test : CodeFixVerifier
    {
        //No diagnostics expected to show up
        [TestMethod]
        public void VeryfyNoDiagnosticWillShowUp_AddMissingAsyncSuffix_Declaration()
        {
            var test = @"";

            VerifyCSharpDiagnostic(test);
        }

        //Diagnostic and CodeFix both triggered and checked for
        [TestMethod]
        public void VerifyTaskMethodAsyncSuffix()
        {
            var test = @"
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Diagnostics;

    namespace ConsoleApplication1
    {
        class TypeName
        {   
            public Task MyTask()
            {
                return Task.Delay(1);
            }
        }
    }";
            var rule = MethodMissingAsyncSuffix_Task_Declaration_Rule.Rule;

            var expected = new DiagnosticResult
            {
                Id = "AANA001",
                Message = String.Format(rule.MessageFormat.ToString(), "MyTask"),
                Severity = rule.DefaultSeverity,
                Locations =
                    new[] {
                            new DiagnosticResultLocation("Test0.cs", 14, 25)
                        }
            };

            VerifyCSharpDiagnostic(test, expected);

            var fixtest = @"
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Diagnostics;

    namespace ConsoleApplication1
    {
        class TypeName
        {   
            public Task MyTaskAsync()
            {
                return Task.Delay(1);
            }
        }
    }";
            VerifyCSharpFix(test, fixtest);
        }

        protected override CodeFixProvider GetCSharpCodeFixProvider()
        {
            return new AddMissingAsyncSuffix_Declaration_CodeFixProvider();
        }

        protected override DiagnosticAnalyzer GetCSharpDiagnosticAnalyzer()
        {
            return new AsyncMethodNameSuffix_Task_Declaration_Analyzer();
        }
    }
}
