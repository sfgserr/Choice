import 'package:auto_route/auto_route.dart';
import 'package:choice/ui/ui.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'bloc/forgot_password_bloc.dart';
import 'forgot_password_widgets.dart';

@RoutePage()
class ForgotPasswordScreen extends StatelessWidget {
  const ForgotPasswordScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: CustomScrollView(
        slivers: [
          SliverAppBar(
            pinned: true,
            centerTitle: true,
            leading: const BackBtn(),
            title: Text(
              AppStrings.restorePassword,
              textAlign: TextAlign.center,
              style: AppTextStyles.appBarTextStyle,
            ),
          ),
          SliverToBoxAdapter(
            child: BlocBuilder<ForgotPasswordBloc, ForgotPasswordState>(
              builder: (context, state) {
                return state.isEmailView
                    ? const FpEmailView()
                    : const FpCodeView();
              },
            ),
          ),
        ],
      ),
    );
  }
}
