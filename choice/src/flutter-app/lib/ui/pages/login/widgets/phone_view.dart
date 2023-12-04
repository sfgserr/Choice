import 'package:choice/ui/components/main_button.dart';
import 'package:choice/ui/pages/login/models/input_widget_model.dart';
import 'package:choice/ui/utils/strings.dart';
import 'package:flutter/material.dart';

import 'input_widget.dart';

class PhoneView extends StatelessWidget {
  PhoneView({
    super.key,
  });

  final TextEditingController phoneController = TextEditingController();

  @override
  Widget build(BuildContext context) {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        // phone
        InputWidget(
          inpwModel: InputWidgetModel(
            label: AppStrings.phoneNumberText,
            hintText: AppStrings.phoneNumberHintText,
            showPrefix: true,
            controller: phoneController,
            keyboardType: TextInputType.phone,
          ),
        ),

        MainButton(
          isEnabled: false,
          text: AppStrings.sendCodeText,
        ),
      ],
    );
  }
}
