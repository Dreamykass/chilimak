use common::lexer::token::Token;

#[test]
fn test_runner() {
    for entry in walkdir::WalkDir::new("tests") {
        let entry = entry.unwrap();
        let path = entry.path().to_string_lossy();

        if path.ends_with(".kl") {
            let source = std::fs::read_to_string(&*path).unwrap();
            let lexer_output_test = std::fs::read_to_string(path.to_string() + ".lex.ron");

            if let Ok(lexer_output_test) = lexer_output_test {
                println!("--lex test: {}", path);

                let lexer_output = common::lexer::lex(&source);

                assert_eq!(
                    lexer_output,
                    ron::from_str::<Vec<Token>>(&lexer_output_test)
                        .expect(&*format!("invalid ron at path: {}.lex.ron\n", path)),
                    "lexer output test failed; path: {}",
                    path
                );
            }
        }
    }
}
