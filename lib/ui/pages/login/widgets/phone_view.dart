import 'package:choice/ui/components/main_button.dart';
import 'package:choice/ui/utils/strings.dart';
import 'package:choice/ui/utils/text_styles.dart';
import 'package:flutter/material.dart';

import 'input_widget.dart';

class PhoneView extends StatelessWidget {
  const PhoneView({super.key});

  @override
  Widget build(BuildContext context) {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        // phone
        InputWidget(
          label: AppStrings.phoneNumberText,
          hintText: AppStrings.phoneNumberHintText,
          showPrefix: true,
        ),

        MainButton(
          isEnabled: false,
          text: AppStrings.sendCodeText,
        ),
      ],
    );
  }
}
