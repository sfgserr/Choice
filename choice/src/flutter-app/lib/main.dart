import 'package:logger/logger.dart';
import 'package:flutter/material.dart';
import 'package:get_it/get_it.dart';
import 'config/router/router.dart';
import 'config/theme/theme.dart';
import 'features/forgot_password/bloc/export_forgot_password_bloc.dart';
import 'features/login/bloc/export_login_bloc.dart';
import 'features/set_new_password/bloc/export_set_new_password_bloc.dart';
import 'repositories/storage/local_storage.dart';

final logger = Logger();

void main() {
  WidgetsFlutterBinding.ensureInitialized();
  GetIt.I.registerSingleton<AppRouter>(AppRouter());
  runApp(const MyApp());
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});

  @override
  Widget build(BuildContext context) {
    final router = GetIt.I<AppRouter>();
    return MultiBlocProvider(
      providers: [
        BlocProvider(create: (_) => LoginBloc()),
        BlocProvider(create: (_) => ForgotPasswordBloc()),
        BlocProvider(create: (_) => SetNewPasswordBloc()),
      ],
      child: MaterialApp.router(
        title: 'Choice',
        theme: AppTheme.lightTheme,
        darkTheme: AppTheme.darkTheme,
        themeMode: LocalStorage.isDarkMode ? ThemeMode.dark : ThemeMode.light,
        debugShowCheckedModeBanner: false,
        routerConfig: router.config(),
      ),
    );
  }
}
