import 'package:choice/ui/pages/login/models/input_widget_model.dart';
import 'package:choice/ui/utils/strings.dart';
import 'package:flutter/material.dart';
import 'package:choice/config/theme/colors.dart';
import 'package:choice/ui/utils/text_styles.dart';
import 'package:provider/provider.dart';

import 'obscure_text_icon.dart';

class InputWidget extends StatelessWidget {
  const InputWidget({
    super.key,
    required this.inpwModel,
  });

  final InputWidgetModel inpwModel;

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.symmetric(horizontal: 16, vertical: 12),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          Text(
            inpwModel.label,
            style: AppTextStyles.bodyMediumTextStyle,
          ),
          const SizedBox(
            height: 8,
          ),
          TextFormField(
            autofocus: Provider.of<bool>(context),
            keyboardType: inpwModel.keyboardType,
            controller: inpwModel.controller,
            onChanged: inpwModel.onChangeTextField,
            obscureText: inpwModel.obscureText,
            decoration: InputDecoration(
              hintText: inpwModel.hintText,
              prefixText: inpwModel.showPrefix ? AppStrings.phonePrefix : '',
              suffixIcon: inpwModel.showSuffix
                  ? GestureDetector(
                      onTap: inpwModel.onSuffixIconTap,
                      child: ObscureTextIcon(
                        obscureText: inpwModel.obscureText,
                      ),
                    )
                  : const SizedBox(),
              suffixIconColor: AppColors.suffixIconColor,
            ),
          ),
        ],
      ),
    );
  }
}
