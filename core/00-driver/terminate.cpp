
#include "00-driver/_driver.hpp"
#include <iostream>
#include <fstream>
#include <toml++/toml.h>

namespace {
  [[noreturn]] void ActuallyTerminate(int _code, std::string _msg) {
    SPDLOG_CRITICAL("terminating with code: {}; and msg: {}", _code, _msg);
    // std::exit(_code);
    // throw std::exception("");
    throw driver::TerminalException(
      fmt::format("driver::TerminalException; chilimak is terminating with "
                  "code {}; and message: {}",
                  _code,
                  _msg));
  }
}

void driver::TerminateOnNoRootDirectory() {
  ActuallyTerminate(2, "Can't find root directory.");
}

void driver::Terminate(int _code) {
  auto rootdir = driver::FindRootDirectory();
  std::string msg = "???";

  try {
    auto table = toml::parse_file(rootdir + "/config/return-codes.toml");
    auto code_str = fmt::format("{:03d}", _code);
    auto val_it = table.find(code_str);
    if (val_it == table.end())
      SPDLOG_CRITICAL("can't find rcode in table; key: '{}'", code_str);
    else
      msg = val_it->second.ref<std::string>();
  }
  catch (const std::exception& e) {
    SPDLOG_CRITICAL("exc when parsing return codes: {}", e.what());
  }

  ActuallyTerminate(_code, msg);
}