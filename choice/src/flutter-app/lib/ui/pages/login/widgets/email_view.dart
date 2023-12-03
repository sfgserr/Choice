import 'package:choice/ui/components/main_button.dart';
import 'package:choice/ui/utils/strings.dart';
import 'package:flutter/material.dart';

import 'input_widget.dart';

class EmailView extends StatelessWidget {
  const EmailView({super.key});

  @override
  Widget build(BuildContext context) {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        // email
        InputWidget(
          label: AppStrings.emailText,
          hintText: AppStrings.inputEmail,
        ),

        // password
        InputWidget(
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
        ),

        MainButton(
          isEnabled: false,
          text: AppStrings.loginText,
        ),
      ],
    );
  }
}
