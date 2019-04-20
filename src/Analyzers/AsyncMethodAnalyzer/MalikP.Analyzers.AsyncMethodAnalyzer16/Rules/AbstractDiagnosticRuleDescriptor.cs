﻿// MIT License
//
// Copyright (c) 2019 Peter Malik.
// 
// File: AbstractDiagnosticRuleDescriptor.cs 
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

using Microsoft.CodeAnalysis;

namespace MalikP.Analyzers.AsyncMethodAnalyzer.Rules
{
    public abstract class AbstractDiagnosticRuleDescriptor
    {
        public const string Naming = "Naming";
        public const string Design = "Design";

        protected AbstractDiagnosticRuleDescriptor()
        {
        }

        protected abstract string InternalDiagnosticId { get; }

        protected abstract string Category { get; }

        protected abstract LocalizableString Title { get; }

        protected abstract LocalizableString MessageFormat { get; }

        protected abstract LocalizableString Description { get; }

        protected abstract DiagnosticSeverity Severity { get; }

        protected static DiagnosticDescriptor Create<T>(T diagnosticRuleDescriptor)
            where T : AbstractDiagnosticRuleDescriptor
        {
            return new DiagnosticDescriptor(
            diagnosticRuleDescriptor.InternalDiagnosticId,
            diagnosticRuleDescriptor.Title,
            diagnosticRuleDescriptor.MessageFormat,
            diagnosticRuleDescriptor.Category,
            diagnosticRuleDescriptor.Severity,
            isEnabledByDefault: true,
            description: diagnosticRuleDescriptor.Description);
        }
    }
}