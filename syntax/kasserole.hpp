#pragma once
#include <tao/pegtl.hpp>

struct postal_address : tao::pegtl::seq<name_part, street, zip_part> {};
struct name_part
  : tao::pegtl::sor<
      tao::pegtl::seq<tao::pegtl::star<personal_part, SP>,
                      last_name,
                      tao::pegtl::opt<tao::pegtl::seq<SP, suffix>>,
                      CRLF>,
      tao::pegtl::seq<personal_part, CRLF>> {};
struct personal_part
  : tao::pegtl::sor<first_name,
                    tao::pegtl::seq<initial, tao::pegtl::one<'.'>>> {};
struct first_name : tao::pegtl::star<tao::pegtl::alpha> {};
struct initial : tao::pegtl::alpha {};
struct last_name : tao::pegtl::star<tao::pegtl::alpha> {};
struct suffix
  : tao::pegtl::sor<
      tao::pegtl::istring<'J', 'r', '.'>,
      tao::pegtl::istring<'S', 'r', '.'>,
      tao::pegtl::plus<tao::pegtl::sor<tao::pegtl::istring<'I'>,
                                       tao::pegtl::istring<'V'>,
                                       tao::pegtl::istring<'X'>>>> {};
struct street
  : tao::pegtl::seq<tao::pegtl::opt<tao::pegtl::seq<apt, SP>>,
                    house_num,
                    SP,
                    street_name,
                    CRLF> {};
struct apt
  : tao::pegtl::seq<tao::pegtl::digit,
                    tao::pegtl::rep_opt<3, tao::pegtl::digit>> {};
struct house_num
  : tao::pegtl::seq<
      tao::pegtl::sor<tao::pegtl::digit, tao::pegtl::alpha>,
      tao::pegtl::
        rep_opt<7, tao::pegtl::sor<tao::pegtl::digit, tao::pegtl::alpha>>> {};
struct street_name : tao::pegtl::plus<tao::pegtl::print> {};
struct zip_part
  : tao::pegtl::seq<town_name,
                    tao::pegtl::one<','>,
                    SP,
                    state,
                    tao::pegtl::seq<SP, tao::pegtl::opt<SP>>,
                    zip_code,
                    CRLF> {};
struct town_name : tao::pegtl::plus<tao::pegtl::sor<tao::pegtl::alpha, SP>> {};
struct state : tao::pegtl::rep<2, tao::pegtl::alpha> {};
struct zip_code
  : tao::pegtl::seq<
      tao::pegtl::rep<5, tao::pegtl::digit>,
      tao::pegtl::opt<tao::pegtl::seq<tao::pegtl::one<'-'>,
                                      tao::pegtl::rep<4, tao::pegtl::digit>>>> {
};
