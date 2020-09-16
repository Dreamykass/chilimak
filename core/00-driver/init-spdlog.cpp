
#include "00-driver/_driver.hpp"
#include "00-driver/common-includes.hpp"

void driver::InitSpdlog() {
  spdlog::set_pattern("[%T]%^ [%=8l] %$[%24s:%3#] %v");

  SPDLOG_INFO("spdlog initalized");
}