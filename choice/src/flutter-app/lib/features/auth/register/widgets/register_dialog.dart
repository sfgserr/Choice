import 'package:choice/features/auth/register/bloc/register_bloc.dart';
import 'package:choice/features/auth/register/widgets/register_finished_dialog.dart';
import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';

import 'register_failure_dialog.dart';

class RegisterDialog extends StatelessWidget {
  const RegisterDialog({
    super.key,
    required this.onFailureTap,
    required this.onTap,
  });

  final Function() onFailureTap;
  final Function() onTap;

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: Colors.transparent,
      body: Container(
        alignment: Alignment.bottomCenter,
        child: BlocBuilder<RegisterBloc, RegisterState>(
          builder: (context, state) {
            if (state is RegisterLoading) {
              return const Center(
                child: CircularProgressIndicator(),
              );
            }
            if (state is RegisterFailure) {
              return RegisterFailureDialog(onTap: onFailureTap);
            }
            return RegisterFinishedDialog(onTap: onTap);
          },
        ),
      ),
    );
  }
}
