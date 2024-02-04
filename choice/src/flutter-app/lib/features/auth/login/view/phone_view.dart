import '../bloc/login_bloc.dart';

import 'package:choice/repositories/models/ui_models/input_widget_model.dart';
import 'package:choice/ui/ui.dart';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:flutter_bloc/flutter_bloc.dart';

class PhoneView extends StatefulWidget {
  const PhoneView({super.key});

  @override
  State<PhoneView> createState() => _PhoneViewState();
}

class _PhoneViewState extends State<PhoneView> {
  late TextEditingController phoneController;

  final _formKey = GlobalKey<FormState>();

  void getCode() {
    if (_formKey.currentState!.validate()) {
      BlocProvider.of<LoginBloc>(context).add(
        GetCode(isGettingCode: true),
      );
    }
  }

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
    bool isKeyboardVisible = Provider.of<bool>(context);
    return BlocBuilder<LoginBloc, LoginState>(
      builder: (context, state) {
        return Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            // phone
            Form(
              key: _formKey,
              child: InputWidget(
                inpwModel: InputWidgetModel(
                  autofocus: isKeyboardVisible,
                  validator: AppValidators.phoneValidator,
                  label: AppStrings.phoneNumberText,
                  hintText: AppStrings.phoneNumberHintText,
                  showPrefix: true,
                  onChangeTextField: (value) {
                    BlocProvider.of<LoginBloc>(context).add(EnableLoginBtn(
                      isLoginBtnEnabled: value.isNotEmpty,
                    ));
                  },
                  onFieldSubmitted: (value) => getCode(),
                  controller: phoneController,
                  keyboardType: TextInputType.phone,
                ),
              ),
            ),

            MainButton(
              isEnabled:
                  BlocProvider.of<LoginBloc>(context).state.isLoginBtnEnabled,
              text: AppStrings.sendCodeText,
              onTap: () => getCode(),
            ),
          ],
        );
      },
    );
  }
}
