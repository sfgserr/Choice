import 'package:choice/config/router/all_pages.dart';
import 'package:choice/features/auth/company_card/bloc/card_company_bloc.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:logger/logger.dart';
import 'package:flutter/material.dart';
import 'package:get_it/get_it.dart';
import 'config/router/router.dart';
import 'config/theme/theme.dart';
import 'features/entry_point/bloc/auth_bloc.dart';
import 'features/auth/register/bloc/register_bloc.dart';
import 'features/auth/forgot_password/bloc/forgot_password_bloc.dart';
import 'features/auth/login/bloc/login_bloc.dart';
import 'features/auth/set_new_password/bloc/set_new_password_bloc.dart';
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
        BlocProvider(create: (_) => CardCompanyBloc()),
      ],
      child: MaterialApp(
        title: 'Choice',
        theme: AppTheme.lightTheme,
        darkTheme: AppTheme.darkTheme,
        themeMode: LocalStorage.isDarkMode ? ThemeMode.dark : ThemeMode.light,
        debugShowCheckedModeBanner: false,
        // routerConfig: router.config(),
        home: CompanyCardScreen(),
      ),
    );
  }
}
