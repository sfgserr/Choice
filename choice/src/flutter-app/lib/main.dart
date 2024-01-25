import 'package:choice/repositories/repositories/user_repository.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:logger/logger.dart';
import 'package:flutter/material.dart';
import 'package:get_it/get_it.dart';
import 'config/router/router.dart';
import 'config/theme/theme.dart';
import 'package:choice/features/entry_point/bloc/auth_bloc.dart';
import 'package:choice/features/register/bloc/register_bloc.dart';
import 'features/forgot_password/bloc/forgot_password_bloc.dart';
import 'features/login/bloc/login_bloc.dart';
import 'features/set_new_password/bloc/set_new_password_bloc.dart';
import 'repositories/storage/local_storage.dart';

final logger = Logger();

void main() async {
  WidgetsFlutterBinding.ensureInitialized();
  GetIt.I.registerSingleton<AppRouter>(AppRouter());
  GetIt.I.registerSingleton<AuthBloc>(AuthBloc());
  await LocalStorage.initStorage();
  runApp(const MyApp());
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});

  @override
  Widget build(BuildContext context) {
    final router = GetIt.I<AppRouter>();
    return MultiBlocProvider(
      providers: [
        BlocProvider(create: (_) => GetIt.I<AuthBloc>()),
        BlocProvider(create: (_) => LoginBloc()),
        BlocProvider(create: (_) => ForgotPasswordBloc()),
        BlocProvider(create: (_) => SetNewPasswordBloc()),
        BlocProvider(create: (_) => RegisterBloc()),
      ],
      child: MaterialApp.router(
        title: 'Choice',
        theme: AppTheme.lightTheme,
        darkTheme: AppTheme.darkTheme,
        themeMode: LocalStorage.isDarkMode ? ThemeMode.dark : ThemeMode.light,
        debugShowCheckedModeBanner: false,
        routerConfig: router.config(),
        // home: EntryPointScreen(),
      ),
    );
  }
}
