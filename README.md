
# chilimak

This will hopefully some day be a compiler for kasserole, targeting LLVM (?).

Parts of the compiler:

0. driver - drives the compiler, running each part of the pipeline
1. pretokenizer - reads source and "stringifies" it, essentially transforming it into a list of strings
2. tokenizer - interprets the list of strings into tokens (type name, variable name, string literal, comma, star, ampersand, bracket, etc)
3. ;
