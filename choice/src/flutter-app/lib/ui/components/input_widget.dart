import 'package:choice/domain/models/ui_models/input_widget_model.dart';
import 'package:choice/ui/utils/strings.dart';
import 'package:flutter/material.dart';
import 'package:choice/config/theme/colors.dart';
import 'package:choice/ui/utils/text_styles.dart';

import '../pages/login/widgets/obscure_text_icon.dart';

class InputWidget extends StatelessWidget {
  const InputWidget({
    super.key,
    required this.inpwModel,
    this.onSuffixTap,
  });

  final InputWidgetModel inpwModel;
  final Function()? onSuffixTap;

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
            autofocus: inpwModel.autofocus,
            keyboardType: inpwModel.keyboardType,
            textInputAction: inpwModel.textInputAction,
            focusNode: inpwModel.focusNode,
            controller: inpwModel.controller,
            onChanged: inpwModel.onChangeTextField,
            obscureText: inpwModel.showSuffix ? inpwModel.obscureText : false,
            onFieldSubmitted: inpwModel.onFieldSubmitted,
            onEditingComplete: () {},
            // maxLength: inpwModel.maxLength,
            decoration: InputDecoration(
              hintText: inpwModel.hintText,
              prefixText: inpwModel.showPrefix ? AppStrings.phonePrefix : '',
              suffixIcon: inpwModel.showSuffix
                  ? GestureDetector(
                      onTap: onSuffixTap,
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
