
#include "00-driver/_driver.hpp"
#include "00-driver/common-includes.hpp"
#include <filesystem>

const std::string driver::FindRootDirectory() {
  auto current = std::filesystem::current_path().string();

  while (std::filesystem::exists(current)) {
    if (std::filesystem::exists(current + "/syntax/kasserole.abnf")) {
      return current + "/";
    } else {
      current += "/../";
    }
  }

  current = std::filesystem::current_path().string();
  SPDLOG_CRITICAL("driver::FindRootDirectory(), cannot find the root");
  SPDLOG_CRITICAL("current_path(): {}", current);
  SPDLOG_CRITICAL("terminating...");
  std::terminate();
}