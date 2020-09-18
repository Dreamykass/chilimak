
#include "00-driver/_driver.hpp"
#include "00-driver/common-includes.hpp"
#include "01-preproc/_preproc.hpp"

#include <fstream>
#include <streambuf>
#include <filesystem>

preproc::SourceString preproc::LoadSourceFromFile(std::string _path) {
  SPDLOG_TRACE("loading source {}", _path);

  if (!std::filesystem::exists(_path)) {
    SPDLOG_CRITICAL("!std::filesystem::exists({})", _path);
    driver::Terminate(100);
  }

  SourceString output;
  output.pathname = _path;

  std::ifstream file(_path);

  file.seekg(0, std::ios::end);
  output.content.reserve(file.tellg());
  file.seekg(0, std::ios::beg);

  output.content.assign((std::istreambuf_iterator<char>(file)),
                        std::istreambuf_iterator<char>());

  SPDLOG_TRACE("loaded source {}", output.pathname);
  SPDLOG_TRACE("first 50 chars: [[[['{}']]]]", output.content.substr(0, 50));
  return output;
}
