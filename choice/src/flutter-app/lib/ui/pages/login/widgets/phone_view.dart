import 'package:choice/domain/blocs/login_bloc/export_login_bloc.dart';
import 'package:choice/ui/components/main_button.dart';
import 'package:choice/domain/models/ui_models/input_widget_model.dart';
import 'package:choice/ui/utils/strings.dart';
import 'package:flutter/material.dart';

import '../../../components/input_widget.dart';

class PhoneView extends StatefulWidget {
  const PhoneView({super.key});

  @override
  State<PhoneView> createState() => _PhoneViewState();
}

class _PhoneViewState extends State<PhoneView> {
  late TextEditingController phoneController;

  @override
  void initState() {
    super.initState();
    phoneController = TextEditingController();
  }

  @override
  void dispose() {
    phoneController.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return BlocBuilder<LoginBloc, LoginState>(
      builder: (context, state) {
        return Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            // phone
            InputWidget(
              inpwModel: InputWidgetModel(
                label: AppStrings.phoneNumberText,
                hintText: AppStrings.phoneNumberHintText,
                showPrefix: true,
                onChangeTextField: (value) {
                  BlocProvider.of<LoginBloc>(context).add(EnableLoginBtn(
                    isLoginBtnEnabled: value.isNotEmpty,
                  ));
                },
                onFieldSubmitted: (value) {
                  BlocProvider.of<LoginBloc>(context).add(GetCode());
                },
                controller: phoneController,
                keyboardType: TextInputType.phone,
              ),
            ),

            MainButton(
              isEnabled:
                  BlocProvider.of<LoginBloc>(context).state.isLoginBtnEnabled,
              text: AppStrings.sendCodeText,
              onTap: () {
                BlocProvider.of<LoginBloc>(context).add(GetCode());
              },
            ),
          ],
        );
      },
    );
  }
}
