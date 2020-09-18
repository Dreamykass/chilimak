
#include "00-driver/common-includes.hpp"

namespace preproc {

  struct SourceString {
    std::string pathname;
    std::string content;
  };

  SourceString LoadSourceFromFile(std::string _path);
}