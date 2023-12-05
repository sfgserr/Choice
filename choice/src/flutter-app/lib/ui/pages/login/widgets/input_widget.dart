import 'package:choice/domain/blocs/input_widget_bloc/export_input_widget_bloc.dart';
import 'package:choice/ui/pages/login/models/input_widget_model.dart';
import 'package:choice/ui/utils/strings.dart';
import 'package:flutter/material.dart';
import 'package:choice/config/theme/colors.dart';
import 'package:choice/ui/utils/text_styles.dart';

import 'obscure_text_icon.dart';

class InputWidget extends StatelessWidget {
  const InputWidget({
    super.key,
    required this.inpwModel,
  });

  final InputWidgetModel inpwModel;

  @override
  Widget build(BuildContext context) {
    return BlocBuilder<InputWidgetBloc, InputWidgetState>(
      builder: (context, state) {
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
                obscureText: inpwModel.showSuffix ? state.obscureText : false,
                onFieldSubmitted: inpwModel.onFieldSubmitted,
                onEditingComplete: () {},
                // maxLength: inpwModel.maxLength,
                decoration: InputDecoration(
                  hintText: inpwModel.hintText,
                  prefixText:
                      inpwModel.showPrefix ? AppStrings.phonePrefix : '',
                  suffixIcon: inpwModel.showSuffix
                      ? GestureDetector(
                          onTap: () {
                            BlocProvider.of<InputWidgetBloc>(context).add(SuffixTap());
                          },
                          child: ObscureTextIcon(
                            obscureText: state.obscureText,
                          ),
                        )
                      : const SizedBox(),
                  suffixIconColor: AppColors.suffixIconColor,
                ),
              ),
            ],
          ),
        );
      },
    );
  }
}
