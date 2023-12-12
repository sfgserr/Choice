import 'package:choice/domain/models/ui_models/input_widget_model.dart';
import 'package:choice/ui/components/main_button.dart';
import 'package:choice/ui/components/input_widget.dart';
import 'package:choice/ui/utils/strings.dart';
import 'package:choice/ui/utils/text_styles.dart';
import 'package:flutter/material.dart';

import '../../../../domain/blocs/forgot_password_bloc/export_forgot_password_bloc.dart';

class FpEmailView extends StatefulWidget {
  const FpEmailView({super.key});

  @override
  State<FpEmailView> createState() => _FpEmailViewState();
}

class _FpEmailViewState extends State<FpEmailView> {
  late TextEditingController emailController;

  void sendCode() {
    FocusScope.of(context).unfocus();
    BlocProvider.of<ForgotPasswordBloc>(context).add(
      ChangeView(
        isEmailView: false,
        currentEmail: emailController.text,
      ),
    );
  }

  @override
  void initState() {
    super.initState();
    emailController = TextEditingController();
  }

  @override
  void dispose() {
    emailController.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return BlocBuilder<ForgotPasswordBloc, ForgotPasswordState>(
      builder: (context, state) {
        return Column(
          crossAxisAlignment: CrossAxisAlignment.center,
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            const SizedBox(
              height: 42,
            ),
            Padding(
              padding: const EdgeInsets.symmetric(horizontal: 16),
              child: Text(
                AppStrings.weWillSendCodeText,
                style: AppTextStyles.bodySmallTextStyle,
              ),
            ),

            // email
            InputWidget(
              inpwModel: InputWidgetModel(
                label: AppStrings.emailText,
                hintText: AppStrings.inputEmail,
                controller: emailController,
                onChangeTextField: (value) {
                  BlocProvider.of<ForgotPasswordBloc>(context).add(
                    EnableMainBtn(isEnabledMainBtn: value.isNotEmpty),
                  );
                },
                onFieldSubmitted: (value) => sendCode,
              ),
            ),

            MainButton(
              isEnabled: state.isEnabledMainBtn,
              text: AppStrings.sendCodeText,
              onTap: sendCode,
            ),
          ],
        );
      },
    );
  }
}
