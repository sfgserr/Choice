import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:choice/features/forgot_password/bloc/forgot_password_bloc.dart';
import 'package:choice/repositories/models/ui_models/input_widget_model.dart';
import 'package:choice/ui/ui.dart';
import 'package:flutter/material.dart';


class FpEmailView extends StatefulWidget {
  const FpEmailView({super.key});

  @override
  State<FpEmailView> createState() => _FpEmailViewState();
}

class _FpEmailViewState extends State<FpEmailView> {
  late TextEditingController emailController;

  final _formKey = GlobalKey<FormState>();

  void sendCode() {
    if (_formKey.currentState!.validate()) {
      FocusScope.of(context).unfocus();
      BlocProvider.of<ForgotPasswordBloc>(context).add(
        ChangeView(
          isEmailView: false,
          currentEmail: emailController.text,
        ),
      );
    }
  }

  @override
  void initState() {
    super.initState();
    emailController = TextEditingController();
  }

  @override
  void dispose() {
    emailController.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return BlocBuilder<ForgotPasswordBloc, ForgotPasswordState>(
      builder: (context, state) {
        return Column(
          crossAxisAlignment: CrossAxisAlignment.center,
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            const SizedBox(
              height: 42,
            ),
            Padding(
              padding: const EdgeInsets.symmetric(horizontal: 16),
              child: Text(
                AppStrings.weWillSendCodeText,
                style: AppTextStyles.bodySmallTextStyle,
              ),
            ),

            // email
            Form(
              key: _formKey,
              child: InputWidget(
                inpwModel: InputWidgetModel(
                  validator: AppValidators.emailValidator,
                  label: AppStrings.emailText,
                  hintText: AppStrings.inputEmail,
                  controller: emailController,
                  onChangeTextField: (value) {
                    BlocProvider.of<ForgotPasswordBloc>(context).add(
                      EnableMainBtn(isEnabledMainBtn: value.isNotEmpty),
                    );
                  },
                  onFieldSubmitted: (value) => sendCode,
                ),
              ),
            ),

            MainButton(
              isEnabled: state.isEnabledMainBtn,
              text: AppStrings.sendCodeText,
              onTap: sendCode,
            ),
          ],
        );
      },
    );
  }
}
