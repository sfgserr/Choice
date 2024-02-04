import 'package:auto_route/auto_route.dart';
import 'package:choice/config/router/all_pages.dart';
import 'package:choice/main.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:choice/features/entry_point/bloc/auth_bloc.dart';
import 'package:choice/features/home/home_screen.dart';
import 'package:flutter/material.dart';

@RoutePage()
class EntryPointScreen extends StatelessWidget {
  const EntryPointScreen({super.key});

  @override
  Widget build(BuildContext context) {
    // auth logic
    return BlocBuilder<AuthBloc, AuthState>(
      builder: (context, state) {
        logger.i("Current state - ${state.toString()}");
        if (state is AuthUninitialized) {
          return const SplashScreen();
        }
        if (state is AuthUnauthenticated) {
          AutoRouter.of(context).popUntilRoot();
          return const LoginScreen();
        }
        if (state is AuthAuthenticated) {
          // AutoRouter.of(context).popUntilRoot();
          return const HomeScreen();
        }
        return const Center(
          child: CircularProgressIndicator(),
        );
      },
    );
  }
}
