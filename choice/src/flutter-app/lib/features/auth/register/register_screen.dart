import 'package:auto_route/auto_route.dart';
import 'package:choice/config/router/router.dart';
import 'package:choice/features/entry_point/bloc/auth_bloc.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:choice/repositories/models/ui_models/input_widget_model.dart';
import 'package:choice/ui/ui.dart';
import 'package:flutter/material.dart';
import 'package:form_field_validator/form_field_validator.dart';
import 'package:get_it/get_it.dart';

import 'bloc/register_bloc.dart';
import 'widgets/register_widgets.dart';

@RoutePage()
class RegisterScreen extends StatefulWidget {
  const RegisterScreen({super.key});

  @override
  State<RegisterScreen> createState() => _RegisterScreenState();
}

class _RegisterScreenState extends State<RegisterScreen> {
  late TextEditingController nameController;
  late TextEditingController surnameController;
  late TextEditingController emailController;
  late TextEditingController passwordController;
  late TextEditingController confirmPasswordController;

  late FocusNode surnameFocus;
  late FocusNode emailFocus;
  late FocusNode passwordFocus;
  late FocusNode confirmPasswordFocus;

  final _formNameKey = GlobalKey<FormState>();
  final _formSurnameKey = GlobalKey<FormState>();
  final _formEmailKey = GlobalKey<FormState>();
  final _formPasswordKey = GlobalKey<FormState>();
  final _formConfirmPasswordKey = GlobalKey<FormState>();

  @override
  void initState() {
    super.initState();
    // init controllers and focusNodes
    surnameController = TextEditingController();
    surnameFocus = FocusNode();

    nameController = TextEditingController();
    emailController = TextEditingController();
    passwordController = TextEditingController();
    confirmPasswordController = TextEditingController();

    emailFocus = FocusNode();
    passwordFocus = FocusNode();
    confirmPasswordFocus = FocusNode();
  }

  @override
  void dispose() {
    nameController.dispose();
    emailController.dispose();
    emailFocus.dispose();
    passwordController.dispose();
    passwordFocus.dispose();
    confirmPasswordController.dispose();
    confirmPasswordFocus.dispose();
    surnameFocus.dispose();
    surnameController.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: BlocBuilder<RegisterBloc, RegisterState>(
        builder: (context, state) {
          double alreadyLoggedInBtnTopPadding =
              state.isCompanyRegister ? 120 : 22;

          return CustomScrollView(
            slivers: [
              SliverAppBar(
                automaticallyImplyLeading: false,
                title: Text(
                  state.isCompanyRegister
                      ? AppStrings.registerCompany
                      : AppStrings.registerClient,
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

                    // name
                    Form(
                      key: _formNameKey,
                      child: InputWidget(
                        inpwModel: InputWidgetModel(
                          validator: (str) => null,
                          autofocus: true,
                          label: AppStrings.nameText,
                          hintText: AppStrings.inputName,
                          controller: nameController,
                          onFieldSubmitted: (value) {
                            if (_formNameKey.currentState!.validate()) {
                              FocusScope.of(context).requestFocus(
                                state.isCompanyRegister
                                    ? emailFocus
                                    : surnameFocus,
                              );
                            }
                          },
                          textInputAction: TextInputAction.next,
                        ),
                      ),
                    ),

                    // surname
                    if (!state.isCompanyRegister)
                      Form(
                        key: _formSurnameKey,
                        child: InputWidget(
                          inpwModel: InputWidgetModel(
                            validator: (str) => null,
                            label: AppStrings.surnameText,
                            hintText: AppStrings.inputSurname,
                            controller: surnameController,
                            focusNode: surnameFocus,
                            onFieldSubmitted: (value) {
                              if (_formSurnameKey.currentState!.validate()) {
                                FocusScope.of(context).requestFocus(emailFocus);
                              }
                            },
                            textInputAction: TextInputAction.next,
                          ),
                        ),
                      ),

                    // email
                    Form(
                      key: _formEmailKey,
                      child: InputWidget(
                        inpwModel: InputWidgetModel(
                          validator: AppValidators.emailValidator,
                          label: AppStrings.emailText,
                          hintText: AppStrings.inputEmail,
                          controller: emailController,
                          focusNode: emailFocus,
                          onFieldSubmitted: (value) {
                            if (_formEmailKey.currentState!.validate()) {
                              FocusScope.of(context)
                                  .requestFocus(passwordFocus);
                            }
                          },
                          textInputAction: TextInputAction.next,
                        ),
                      ),
                    ),

                    // password
                    Form(
                      key: _formPasswordKey,
                      child: InputWidget(
                        inpwModel: InputWidgetModel(
                          validator: AppValidators.passwordValidator,
                          label: AppStrings.passwordText,
                          hintText: AppStrings.inputPassword,
                          onFieldSubmitted: (value) {
                            if (_formPasswordKey.currentState!.validate()) {
                              FocusScope.of(context)
                                  .requestFocus(confirmPasswordFocus);
                            }
                          },
                          onChangeTextField: (value) {
                            BlocProvider.of<RegisterBloc>(context)
                                .add(UpdateFirstPassword(newPassword: value));
                          },
                          showSuffix: true,
                          obscureText: state.isObscurePassword,
                          controller: passwordController,
                          focusNode: passwordFocus,
                          textInputAction: TextInputAction.next,
                        ),
                        onSuffixTap: () {
                          BlocProvider.of<RegisterBloc>(context)
                              .add(ObscurePasswordText());
                        },
                      ),
                    ),

                    // confirm password
                    Form(
                      key: _formConfirmPasswordKey,
                      child: InputWidget(
                        inpwModel: InputWidgetModel(
                          validator: (val) => MatchValidator(
                                  errorText:
                                      AppStrings.passwordsAreNotEqualText)
                              .validateMatch(val!, state.firstPassword),
                          label: AppStrings.repeatPassword,
                          hintText: AppStrings.inputPassword,
                          onChangeTextField: (value) {
                            BlocProvider.of<RegisterBloc>(context).add(
                              EnableMainBtn(isMainBtnEnabled: value.isNotEmpty),
                            );
                          },
                          onFieldSubmitted: (value) =>
                              signUpTap(state, context),
                          showSuffix: true,
                          obscureText: state.isObscureConfirmPassword,
                          controller: confirmPasswordController,
                          focusNode: confirmPasswordFocus,
                        ),
                        onSuffixTap: () {
                          BlocProvider.of<RegisterBloc>(context)
                              .add(ObscureConfirmPasswordText());
                        },
                      ),
                    ),

                    MainButton(
                      isEnabled: state.isMainBtnEnabled,
                      text: AppStrings.createAccountText,
                      onTap: () => signUpTap(state, context),
                    ),
                  ],
                ),
              ),
              SliverPadding(
                padding: EdgeInsets.fromLTRB(
                    110, alreadyLoggedInBtnTopPadding, 110, 30),
                sliver: SliverToBoxAdapter(
                  child: Column(
                    children: [
                      Text(
                        AppStrings.alreadyLoggedIn,
                        textAlign: TextAlign.center,
                        style: AppTextStyles.logoTextStyle,
                      ),
                      MyTextButton(
                        text: AppStrings.loginText,
                        padding: const EdgeInsets.only(top: 6),
                        onTap: () => AutoRouter.of(context).pop(),
                      ),
                    ],
                  ),
                ),
              ),
            ],
          );
        },
      ),
    );
  }

  void signUpTap(state, context) {
    if (state.isMainBtnEnabled &&
        _formPasswordKey.currentState!.validate() &&
        _formConfirmPasswordKey.currentState!.validate()) {
      // close the keyboard
      FocusScope.of(context).unfocus();

      if (!state.isCompanyRegister) {
        // client register
        BlocProvider.of<RegisterBloc>(context).add(
          SignUpTap(
            password: passwordController.text,
            email: emailController.text,
            name: nameController.text,
            surname: surnameController.text,
          ),
        );

        showCupertinoDialog(
          context: context,
          builder: (_) {
            return RegisterDialog(
              onTap: () {
                Navigator.pop(context);
                AutoRouter.of(context).popUntilRoot();
              },
              onFailureTap: () => Navigator.pop(context),
            );
          },
        );
      } else {
        // company register
        // go to company card screen
        AutoRouter.of(context).push(const CompanyCardRoute());
      }
    }
  }
}
