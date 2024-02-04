// GENERATED CODE - DO NOT MODIFY BY HAND

// **************************************************************************
// AutoRouterGenerator
// **************************************************************************

// ignore_for_file: type=lint
// coverage:ignore-file

part of 'router.dart';

abstract class _$AppRouter extends RootStackRouter {
  // ignore: unused_element
  _$AppRouter({super.navigatorKey});

  @override
  final Map<String, PageFactory> pagesMap = {
    CompanyCardRoute.name: (routeData) {
      return AutoRoutePage<dynamic>(
        routeData: routeData,
        child: const CompanyCardScreen(),
      );
    },
    EntryPointRoute.name: (routeData) {
      return AutoRoutePage<dynamic>(
        routeData: routeData,
        child: const EntryPointScreen(),
      );
    },
    ForgotPasswordRoute.name: (routeData) {
      return AutoRoutePage<dynamic>(
        routeData: routeData,
        child: const ForgotPasswordScreen(),
      );
    },
    LoginRoute.name: (routeData) {
      return AutoRoutePage<dynamic>(
        routeData: routeData,
        child: const LoginScreen(),
      );
    },
    RegisterRoute.name: (routeData) {
      return AutoRoutePage<dynamic>(
        routeData: routeData,
        child: const RegisterScreen(),
      );
    },
    SetNewPasswordRoute.name: (routeData) {
      return AutoRoutePage<dynamic>(
        routeData: routeData,
        child: const SetNewPasswordScreen(),
      );
    },
  };
}

/// generated route for
/// [CompanyCardScreen]
class CompanyCardRoute extends PageRouteInfo<void> {
  const CompanyCardRoute({List<PageRouteInfo>? children})
      : super(
          CompanyCardRoute.name,
          initialChildren: children,
        );

  static const String name = 'CompanyCardRoute';

  static const PageInfo<void> page = PageInfo<void>(name);
}

/// generated route for
/// [EntryPointScreen]
class EntryPointRoute extends PageRouteInfo<void> {
  const EntryPointRoute({List<PageRouteInfo>? children})
      : super(
          EntryPointRoute.name,
          initialChildren: children,
        );

  static const String name = 'EntryPointRoute';

  static const PageInfo<void> page = PageInfo<void>(name);
}

/// generated route for
/// [ForgotPasswordScreen]
class ForgotPasswordRoute extends PageRouteInfo<void> {
  const ForgotPasswordRoute({List<PageRouteInfo>? children})
      : super(
          ForgotPasswordRoute.name,
          initialChildren: children,
        );

  static const String name = 'ForgotPasswordRoute';

  static const PageInfo<void> page = PageInfo<void>(name);
}

/// generated route for
/// [LoginScreen]
class LoginRoute extends PageRouteInfo<void> {
  const LoginRoute({List<PageRouteInfo>? children})
      : super(
          LoginRoute.name,
          initialChildren: children,
        );

  static const String name = 'LoginRoute';

  static const PageInfo<void> page = PageInfo<void>(name);
}

/// generated route for
/// [RegisterScreen]
class RegisterRoute extends PageRouteInfo<void> {
  const RegisterRoute({List<PageRouteInfo>? children})
      : super(
          RegisterRoute.name,
          initialChildren: children,
        );

  static const String name = 'RegisterRoute';

  static const PageInfo<void> page = PageInfo<void>(name);
}

/// generated route for
/// [SetNewPasswordScreen]
class SetNewPasswordRoute extends PageRouteInfo<void> {
  const SetNewPasswordRoute({List<PageRouteInfo>? children})
      : super(
          SetNewPasswordRoute.name,
          initialChildren: children,
        );

  static const String name = 'SetNewPasswordRoute';

  static const PageInfo<void> page = PageInfo<void>(name);
}
