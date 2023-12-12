import 'package:auto_route/auto_route.dart';
import 'package:choice/config/router/router.dart';
import 'package:choice/domain/blocs/set_new_password_bloc/export_set_new_password_bloc.dart';
import 'package:choice/domain/models/ui_models/input_widget_model.dart';
import 'package:choice/ui/utils/strings.dart';
import 'package:choice/ui/utils/text_styles.dart';
import 'package:flutter/material.dart';
import 'set_new_password_widgets.dart';

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

  void sendCode(bool isMainBtnEnabled) {
    if (isMainBtnEnabled) {
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
                    InputWidget(
                      inpwModel: InputWidgetModel(
                        label: AppStrings.passwordText,
                        hintText: AppStrings.inputPassword,
                        onFieldSubmitted: (value) {
                          FocusScope.of(context)
                              .requestFocus(confirmPasswordFocus);
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

                    // confirm password
                    InputWidget(
                      inpwModel: InputWidgetModel(
                        label: AppStrings.repeatPassword,
                        hintText: AppStrings.inputPassword,
                        onChangeTextField: (value) {
                          BlocProvider.of<SetNewPasswordBloc>(context).add(
                            EnableMainBtn(
                              isMainBtnEnabled: value.isNotEmpty,
                            ),
                          );
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
