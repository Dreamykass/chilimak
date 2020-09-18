
#include "00-driver/_driver.hpp"
#include "00-driver/common-includes.hpp"
#include <filesystem>

std::string driver::FindRootDirectory() {
  auto current = std::filesystem::current_path().string();

  while (std::filesystem::exists(current)) {
    if (std::filesystem::exists(current + "/syntax/kasserole.abnf")) {
      return current + "/";
    } else {
      current += "/../";
    }
  }

  current = std::filesystem::current_path().string();
  spdlog::set_pattern("[%T.%e]%^ [%=8l] %$[%24s:%3#] **************** %v");
  SPDLOG_CRITICAL("driver::FindRootDirectory(), cannot find the root");
  SPDLOG_CRITICAL("current_path(): {}", current);
  SPDLOG_CRITICAL("terminating...");
  driver::TerminateOnNoRootDirectory();
}