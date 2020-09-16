
#include "abnf2pegtl.hpp"
#include "00-driver/_driver.hpp"
#include "00-driver/common-includes.hpp"
#include <filesystem>
#include <iostream>
#include <fstream>

int main() {
  driver::InitSpdlog();
  auto root_dir = driver::FindRootDirectory();
  using namespace TAO_PEGTL_NAMESPACE;
  SPDLOG_INFO("generator starts");

  auto grammar_filename = root_dir + "/syntax/kasserole.abnf";
  if (!std::filesystem::exists(grammar_filename)) {
    SPDLOG_CRITICAL("{} doesn't exist", grammar_filename);
    SPDLOG_CRITICAL("terminating...");
    std::terminate();
  }

  file_input in(grammar_filename);
  std::stringstream output_sstream;

  try {
    const auto root = parse_tree::
      parse<abnf::grammar::rulelist, abnf::selector, nothing, abnf::control>(
        in);
    SPDLOG_INFO("parsed");

    for (const auto& rule : root->children) {
      abnf::rules_defined.push_back(abnf::get_rulename(rule->children.front()));
    }
    SPDLOG_INFO("defined");

    for (const auto& rule : root->children) {
      // std::cout << abnf::to_string(rule) << '\n';
      output_sstream << abnf::to_string(rule) << '\n';
    }
    SPDLOG_INFO("saved to stream");

  } catch (const parse_error& e) {
    SPDLOG_CRITICAL("generating/parsing error");
    const auto p = e.positions().front();
    std::cerr << e.what() << '\n'
              << in.line_at(p) << '\n'
              << std::setw(p.column) << '^' << '\n';
    SPDLOG_CRITICAL("terminating...");
    std::terminate();
  }

  try {
    auto target_filename = root_dir + "syntax/kasserole.hpp";

    SPDLOG_INFO("saving to {}", target_filename);

    std::ofstream output_file(target_filename, std::ios_base::trunc);

    output_file << "#pragma once\n";
    output_file << "#include <tao/pegtl.hpp>\n";
    output_file << "\n";
    output_file << output_sstream.rdbuf();
    output_file << "\n";

    SPDLOG_INFO("saved");

  } catch (const std::exception& e) {
    SPDLOG_CRITICAL("generating/saving error");
    SPDLOG_CRITICAL("terminating...");
    std::terminate();
  }

  SPDLOG_INFO("generating done");
  return 0;
}