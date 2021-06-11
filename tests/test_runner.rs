use common::lexer::token::Token;

#[test]
fn test_runner() {
    for entry in walkdir::WalkDir::new("tests") {
        let entry = entry.unwrap();
        let path = entry.path().to_string_lossy();

        // if file is a kasserole source
        if path.ends_with(".kl") {
            let source = std::fs::read_to_string(&*path).unwrap();
            let lexer_output = common::lexer::lex(&source);
            let parser_output = common::parser::parse(&lexer_output);

            // lexer output test
            if let Ok(lexer_output_test) = std::fs::read_to_string(path.to_string() + ".lex.ron") {
                println!("--lex test: {}", path);

                assert_eq!(
                    lexer_output,
                    ron::from_str::<Vec<Token>>(&lexer_output_test)
                        .expect(&*format!("invalid ron at path: {}.lex.ron\n", path)),
                    "lexer output test failed; path: {}",
                    path
                );
            }

            // println!("{}", ron::to_string(&parser_output.unwrap()).unwrap());
            println!(
                "{}",
                serde_json::to_string_pretty(&parser_output.unwrap()).unwrap()
            );
        }
    }
}
