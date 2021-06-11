use crate::ast::Definition;
use crate::lexer::token::Token;
use lalrpop_util::lalrpop_mod;

lalrpop_mod!(pub grammar);

pub type ParserError = lalrpop_util::ParseError<usize, Token, ()>;

pub fn parse(tokens: &[Token]) -> Result<Vec<Definition>, ParserError> {
    let tokens: Vec<(usize, Token, usize)> = tokens
        .iter()
        .cloned()
        .map(|t| (0usize, t, 0usize))
        .collect();
    grammar::DefinitionListParser::new().parse(tokens)
}
