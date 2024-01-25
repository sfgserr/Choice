import 'package:auto_route/auto_route.dart';
import 'package:choice/repositories/models/ui_models/input_widget_model.dart';

import 'package:choice/ui/ui.dart';
import 'package:flutter/material.dart';
import 'bloc/set_new_password_bloc.dart';
import 'package:flutter_bloc/flutter_bloc.dart';


@RoutePage()
class SetNewPasswordScreen extends StatefulWidget {
  const SetNewPasswordScreen({super.key});

  @override
  State<SetNewPasswordScreen> createState() => _SetNewPasswordScreenState();
}

class _SetNewPasswordScreenState extends State<SetNewPasswordScreen> {
  late TextEditingController passwordController;
  late TextEditingController confirmPasswordController;
  late FocusNode confirmPasswordFocus;

  final _formKey = GlobalKey<FormState>();
  final _formKey2 = GlobalKey<FormState>();

  void sendCode(bool isMainBtnEnabled) {
    if (isMainBtnEnabled &&
        _formKey.currentState!.validate() &&
        _formKey2.currentState!.validate()) {
      // action
      FocusScope.of(context).unfocus();
      AutoRouter.of(context).popUntil(
        (route) => route.settings.name == 'LoginRoute',
      );
    }
  }

  @override
  void initState() {
    super.initState();
    passwordController = TextEditingController();
    confirmPasswordController = TextEditingController();
    confirmPasswordFocus = FocusNode();
  }

  @override
  void dispose() {
    passwordController.dispose();
    confirmPasswordController.dispose();
    confirmPasswordFocus.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return BlocBuilder<SetNewPasswordBloc, SetNewPasswordState>(
      builder: (context, state) {
        return Scaffold(
          body: CustomScrollView(
            slivers: [
              SliverAppBar(
                automaticallyImplyLeading: false,
                title: Text(
                  AppStrings.setNewPasswordText,
                  style: AppTextStyles.appBarSecondTextStyle,
                ),
              ),
              SliverToBoxAdapter(
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.center,
                  mainAxisAlignment: MainAxisAlignment.center,
                  children: [
                    const SizedBox(
                      height: 10,
                    ),

                    // password
                    Form(
                      key: _formKey,
                      child: InputWidget(
                        inpwModel: InputWidgetModel(
                          validator: AppValidators.passwordValidator,
                          label: AppStrings.passwordText,
                          hintText: AppStrings.inputPassword,
                          onFieldSubmitted: (value) {
                            FocusScope.of(context)
                                .requestFocus(confirmPasswordFocus);
                            _formKey.currentState!.validate();
                          },
                          onChangeTextField: (value) {
                            BlocProvider.of<SetNewPasswordBloc>(context)
                                .add(UpdateFirstPassword(newPassword: value));
                          },
                          showSuffix: true,
                          obscureText: state.isObscurePassword,
                          controller: passwordController,
                          textInputAction: TextInputAction.next,
                        ),
                        onSuffixTap: () {
                          BlocProvider.of<SetNewPasswordBloc>(context)
                              .add(ObscurePasswordText());
                        },
                      ),
                    ),

                    // confirm password
                    Form(
                      key: _formKey2,
                      child: InputWidget(
                        inpwModel: InputWidgetModel(
                          validator: AppValidators.confirmPasswordValidator,
                          label: AppStrings.repeatPassword,
                          hintText: AppStrings.inputPassword,
                          onChangeTextField: (value) {
                            BlocProvider.of<SetNewPasswordBloc>(context).add(
                              EnableMainBtn(isMainBtnEnabled: value.isNotEmpty),
                            );
                            BlocProvider.of<SetNewPasswordBloc>(context)
                                .add(UpdateSecondPassword(newPassword: value));
                          },
                          onFieldSubmitted: (value) =>
                              sendCode(state.isMainBtnEnabled),
                          showSuffix: true,
                          obscureText: state.isObscureConfirmPassword,
                          controller: confirmPasswordController,
                          focusNode: confirmPasswordFocus,
                        ),
                        onSuffixTap: () {
                          BlocProvider.of<SetNewPasswordBloc>(context)
                              .add(ObscureConfirmPasswordText());
                        },
                      ),
                    ),

                    MainButton(
                      isEnabled: state.isMainBtnEnabled,
                      text: AppStrings.saveNewPassword,
                      onTap: () => sendCode(state.isMainBtnEnabled),
                    ),
                  ],
                ),
              ),
            ],
          ),
        );
      },
    );
  }
}
