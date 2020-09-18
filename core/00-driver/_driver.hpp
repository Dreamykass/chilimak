#pragma once

#include <string>
#include "00-driver/common-includes.hpp"

namespace driver {

  void InitSpdlog(spdlog::level::level_enum _con_level = spdlog::level::info,
                  spdlog::level::level_enum _file_level = spdlog::level::trace);
  std::string FindRootDirectory();

  struct TerminalException : public std::exception {
    std::string str;
    TerminalException(std::string _str)
      : str(_str) {}
    virtual const char* what() const override { return str.c_str(); }
  };

  [[noreturn]] void TerminateOnNoRootDirectory();
  [[noreturn]] void Terminate(int _code);

}
