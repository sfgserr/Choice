import 'package:auto_route/auto_route.dart';
import 'package:choice/config/theme/colors.dart';
import 'package:choice/ui/utils/strings.dart';
import 'package:choice/ui/utils/text_styles.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:flutter_keyboard_visibility/flutter_keyboard_visibility.dart';
import 'package:provider/provider.dart';

import 'bloc/export_forgot_password_bloc.dart';
import 'forgot_password_widgets.dart';

@RoutePage()
class ForgotPasswordScreen extends StatelessWidget {
  const ForgotPasswordScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return BlocBuilder<ForgotPasswordBloc, ForgotPasswordState>(
      builder: (context, state) {
        return Scaffold(
          body: KeyboardVisibilityBuilder(
            builder: (context, isKeyboardVisible) {
              return Provider.value(
                value: isKeyboardVisible,
                child: CustomScrollView(
                  slivers: [
                    SliverAppBar(
                      pinned: true,
                      centerTitle: true,
                      leading: InkWell(
                        onTap: () => AutoRouter.of(context).pop(),
                        child: Icon(
                          CupertinoIcons.chevron_left,
                          size: 28,
                          color: AppColors.primaryColor,
                        ),
                      ),
                      title: Text(
                        AppStrings.restorePassword,
                        textAlign: TextAlign.center,
                        style: AppTextStyles.appBarTextStyle,
                      ),
                    ),
                    SliverToBoxAdapter(
                      child: state.isEmailView
                          ? const FpEmailView()
                          : const FpCodeView(),
                    ),
                  ],
                ),
              );
            },
          ),
        );
      },
    );
  }
}
