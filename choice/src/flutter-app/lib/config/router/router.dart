import 'package:auto_route/auto_route.dart';
import 'package:choice/config/router/all_pages.dart';

part 'router.gr.dart';

@AutoRouterConfig()
class AppRouter extends _$AppRouter {

  @override
  List<AutoRoute> get routes => [
    // routes
    // AutoRoute(page: EntryPoint)
    AutoRoute(page: SplashRoute.page, path: '/'),
    AutoRoute(page: LoginRoute.page),
    AutoRoute(page: ForgotPasswordRoute.page),
  ];
}

