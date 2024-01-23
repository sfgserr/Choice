import 'package:choice/features/entry_point/bloc/auth_bloc.dart';
import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';

class HomeScreen extends StatelessWidget {
  const HomeScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: Colors.blue,
      body: Center(
        child: InkWell(
          onTap: () {
            BlocProvider.of<AuthBloc>(context).add(LoggedOut());
          },
          child: const Text(
            'Home Screen',
            style: TextStyle(
              color: Colors.white,
              fontWeight: FontWeight.w700,
              fontSize: 40,
            ),
          ),
        ),
      ),
    );
  }
}
