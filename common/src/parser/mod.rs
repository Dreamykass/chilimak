use crate::lexer::token::Token;
use lalrpop_util::lalrpop_mod;

lalrpop_mod!(pub grammar);

pub fn parse(tokens: Vec<Token>) {
    let ast =
        grammar::DefinitionListParser::new().parse(tokens.into_iter().map(|t| (0usize, t, 0usize)));
    println!("{:?}", ast);
}
