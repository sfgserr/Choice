import 'package:choice/repositories/models/ui_models/input_widget_model.dart';
import 'package:flutter/material.dart';
import 'package:choice/ui/ui.dart';

class ObscureTextIcon extends StatelessWidget {
  const ObscureTextIcon({super.key, required this.obscureText});

  final bool obscureText;

  @override
  Widget build(BuildContext context) {
    return obscureText
        ? const Icon(Icons.visibility_rounded)
        : const Icon(Icons.visibility_off_rounded);
  }
}

class InputWidget extends StatelessWidget {
  const InputWidget({
    super.key,
    required this.inpwModel,
    this.onSuffixTap,
    this.needPadding = true,
  });

  final InputWidgetModel inpwModel;
  final Function()? onSuffixTap;
  final bool needPadding;

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding:
          EdgeInsets.symmetric(horizontal: needPadding ? 16 : 0, vertical: 12),
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
          Stack(
            children: [
              TextFormField(
                validator: inpwModel.validator,
                autofocus: inpwModel.autofocus,
                keyboardType: inpwModel.keyboardType,
                textInputAction: inpwModel.textInputAction,
                focusNode: inpwModel.focusNode,
                controller: inpwModel.controller,
                onChanged: inpwModel.onChangeTextField,
                obscureText:
                    inpwModel.showSuffix ? inpwModel.obscureText : false,
                onFieldSubmitted: inpwModel.onFieldSubmitted,
                onEditingComplete: () {},
                maxLines: inpwModel.maxLines,
                // maxLength: inpwModel.maxLength,
                decoration: InputDecoration(
                  contentPadding: inpwModel.showPrefix
                      ? const EdgeInsets.fromLTRB(68, 12, 12, 12)
                      : const EdgeInsets.symmetric(
                          horizontal: 12, vertical: 12),
                  hintText: inpwModel.hintText,
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
              if (inpwModel.showPrefix)
                Positioned(
                  top: 13,
                  child: Row(
                    children: [
                      const SizedBox(
                        width: 8,
                      ),
                      SizedBox(
                        width: 40,
                        child: Text(
                          AppStrings.phonePrefix,
                          textAlign: TextAlign.center,
                          style: AppTextStyles.hintTextStyle.copyWith(
                            color: Colors.black,
                          ),
                        ),
                      ),
                      const SizedBox(
                        width: 8,
                      ),
                      Container(
                        width: 0.50,
                        height: 24,
                        decoration: BoxDecoration(
                          color: Colors.black.withOpacity(0.5),
                        ),
                      ),
                    ],
                  ),
                ),
            ],
          )
        ],
      ),
    );
  }
}
