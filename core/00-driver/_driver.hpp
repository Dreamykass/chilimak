#pragma once

#include <string>
#include "00-driver/common-includes.hpp"

namespace driver {

  void InitSpdlog(spdlog::level::level_enum _con_level = spdlog::level::info,
                  spdlog::level::level_enum _file_level = spdlog::level::trace);
  std::string FindRootDirectory();

}
