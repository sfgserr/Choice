import 'package:auto_route/auto_route.dart';
import 'package:choice/config/router/router.dart';
import 'package:choice/features/login/bloc/login_bloc.dart';
import 'package:choice/repositories/models/ui_models/input_widget_model.dart';
import 'package:choice/ui/ui.dart';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:flutter_bloc/flutter_bloc.dart';

class EmailView extends StatefulWidget {
  const EmailView({super.key});

  @override
  State<EmailView> createState() => _EmailViewState();
}

class _EmailViewState extends State<EmailView> {
  late TextEditingController emailController;
  late TextEditingController passwordController;
  late FocusNode emailFocus;
  late FocusNode passwordFocus;

  final _formKey = GlobalKey<FormState>();
  final _formKey2 = GlobalKey<FormState>();

  @override
  void initState() {
    super.initState();
    emailController = TextEditingController();
    passwordController = TextEditingController();
    emailFocus = FocusNode();
    passwordFocus = FocusNode();
  }

  @override
  void dispose() {
    emailController.dispose();
    passwordController.dispose();
    emailFocus.dispose();
    passwordFocus.dispose();
    super.dispose();
  }

  void loginTap(String email, String password) {
    if (_formKey.currentState!.validate() &&
        _formKey2.currentState!.validate()) {
      // TODO
      BlocProvider.of<LoginBloc>(context).add(LoginTap(
        email: email,
        password: password,
      ));
      if (BlocProvider.of<LoginBloc>(context).state is LoginFailure) {
        snackbar();
      }
    }
  }

  void snackbar() {
    ScaffoldMessenger.of(context).showSnackBar(
      const SnackBar(
        duration: Duration(seconds: 1),
        backgroundColor: Colors.red,
        content: Text(
          'Неверный пароль!',
        ),
      ),
    );
  }

  @override
  Widget build(BuildContext context) {
    bool isKeyboardVisible = Provider.of<bool>(context);
    return BlocBuilder<LoginBloc, LoginState>(
      builder: (context, state) {
        return Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            // email
            Form(
              key: _formKey,
              child: InputWidget(
                inpwModel: InputWidgetModel(
                  validator: AppValidators.emailValidator,
                  autofocus: isKeyboardVisible,
                  label: AppStrings.emailText,
                  hintText: AppStrings.inputEmail,
                  controller: emailController,
                  focusNode: emailFocus,
                  onFieldSubmitted: (value) {
                    if (_formKey.currentState!.validate()) {
                      FocusScope.of(context).requestFocus(passwordFocus);
                    }
                  },
                  textInputAction: TextInputAction.next,
                ),
              ),
            ),

            // password
            Form(
              key: _formKey2,
              child: InputWidget(
                inpwModel: InputWidgetModel(
                  validator: AppValidators.passwordValidator,
                  label: AppStrings.passwordText,
                  hintText: AppStrings.inputPassword,
                  onChangeTextField: (value) {
                    BlocProvider.of<LoginBloc>(context).add(EnableLoginBtn(
                      isLoginBtnEnabled: value.isNotEmpty,
                    ));
                  },
                  onFieldSubmitted: (value) => loginTap(
                    emailController.text,
                    passwordController.text,
                  ),
                  showSuffix: true,
                  obscureText: state.isObscurePassword,
                  controller: passwordController,
                  focusNode: passwordFocus,
                ),
                onSuffixTap: () {
                  BlocProvider.of<LoginBloc>(context).add(
                    ObscurePasswordText(),
                  );
                },
              ),
            ),

            MainButton(
              isEnabled: state.isLoginBtnEnabled,
              text: AppStrings.loginText,
              onTap: () => loginTap(
                emailController.text,
                passwordController.text,
              ),
            ),

            if (isKeyboardVisible)
              MyTextButton(
                text: AppStrings.forgotPassword,
                onTap: () {
                  FocusScope.of(context).unfocus();
                  AutoRouter.of(context).push(const ForgotPasswordRoute());
                },
              ),
          ],
        );
      },
    );
  }
}
