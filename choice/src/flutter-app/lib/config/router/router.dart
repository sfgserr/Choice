import 'package:auto_route/auto_route.dart';
import 'package:choice/config/router/all_pages.dart';
import 'package:flutter/material.dart';

part 'router.gr.dart';

@AutoRouterConfig()
class AppRouter extends _$AppRouter {

  @override
  List<AutoRoute> get routes => [
    // routes
    AutoRoute(page: EntryPointRoute.page, path: '/'),
    // AutoRoute(page: LoginRoute.page),
    AutoRoute(page: ForgotPasswordRoute.page),
    AutoRoute(page: SetNewPasswordRoute.page),
    AutoRoute(page: RegisterRoute.page),
    AutoRoute(page: CompanyCardRoute.page),
  ];
}

