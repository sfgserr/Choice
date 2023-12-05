import 'package:choice/ui/components/main_button.dart';
import 'package:choice/ui/pages/login/models/input_widget_model.dart';
import 'package:choice/ui/utils/strings.dart';
import 'package:flutter/material.dart';

import 'input_widget.dart';

class EmailView extends StatelessWidget {
  EmailView({
    super.key,
  });

  final TextEditingController emailController = TextEditingController();
  final TextEditingController passwordController = TextEditingController();

  @override
  Widget build(BuildContext context) {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        // email
        InputWidget(
          inpwModel: InputWidgetModel(
            label: AppStrings.emailText,
            hintText: AppStrings.inputEmail,
            controller: emailController,
          ),
        ),

        // password
        InputWidget(
          inpwModel: InputWidgetModel(
            label: AppStrings.passwordText,
            hintText: AppStrings.inputPassword,
            onSuffixIconTap: () {
              // change obscureText value
            },
            onChangeTextField: (value) {
              if (value.isNotEmpty) {
                // change isButtonEnabled value
              }
            },
            obscureText: false,
            showSuffix: true,
            controller: passwordController,
          ),
        ),

        MainButton(
          isEnabled: false,
          text: AppStrings.loginText,
        ),
      ],
    );
  }
}
