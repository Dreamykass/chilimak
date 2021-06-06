use crate::lexer::token::Token;
use lalrpop_util::lalrpop_mod;

lalrpop_mod!(pub grammar);

// #[allow(clippy::style, clippy::complexity, clippy::perf)]
// mod grammar {
//     include!(concat!(env!("OUT_DIR"), "/grammar.rs"));
// }

pub fn parse(tokens: Vec<Token>) {
    let tokens: Vec<(usize, Token, usize)> =
        tokens.into_iter().map(|t| (0usize, t, 0usize)).collect();
    let ast = grammar::DefinitionListParser::new().parse(tokens);
    println!("{:?}", ast);
}
