import 'package:choice/ui/utils/strings.dart';
import 'package:flutter/material.dart';
import 'package:choice/config/theme/colors.dart';
import 'package:choice/ui/utils/text_styles.dart';

import 'obscure_text_icon.dart';

class InputWidget extends StatelessWidget {
  const InputWidget({
    super.key,
    required this.label,
    required this.hintText,
    this.showPrefix = false,
    this.showSuffix = false,
    this.obscureText = false,
    this.onSuffixIconTap,
    this.onChangeTextField,
  });

  final String label;
  final String hintText;
  final Function()? onSuffixIconTap;
  final Function(String)? onChangeTextField;
  final bool showPrefix;
  final bool showSuffix;
  final bool obscureText;

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.symmetric(horizontal: 16, vertical: 12),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          Text(
            label,
            style: AppTextStyles.bodyMediumTextStyle,
          ),
          const SizedBox(
            height: 8,
          ),
          TextFormField(
            onChanged: onChangeTextField,
            obscureText: obscureText,
            decoration: InputDecoration(
              isDense: true,
              hintText: hintText,
              prefixText: showPrefix ? AppStrings.phonePrefix : '',
              prefixStyle: AppTextStyles.hintTextStyle.copyWith(
                color: Colors.black,
              ),
              suffixIcon: showSuffix
                  ? GestureDetector(
                      onTap: onSuffixIconTap,
                      child: ObscureTextIcon(
                        obscureText: obscureText,
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
