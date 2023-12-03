import 'dart:async';

import 'package:choice/ui/utils/text_styles.dart';
import 'package:choice/ui/utils/strings.dart';
import 'package:flutter/material.dart';
import 'package:flutter_keyboard_visibility/flutter_keyboard_visibility.dart';
import 'login_widgets.dart';

class LoginScreen extends StatefulWidget {
  const LoginScreen({super.key});

  @override
  State<LoginScreen> createState() => _LoginScreenState();
}

class _LoginScreenState extends State<LoginScreen>
    with TickerProviderStateMixin {
  late TabController _tabController;

  bool isEmailTab = true;

  @override
  void initState() {
    super.initState();
    _tabController = TabController(length: 2, vsync: this);
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: KeyboardVisibilityBuilder(
        builder: (context, isKeyboardVisible) {
          return CustomScrollView(
            slivers: [
              SliverToBoxAdapter(
                child: Column(
                  children: [
                    !isKeyboardVisible
                        ? const Column(
                            crossAxisAlignment: CrossAxisAlignment.center,
                            children: [
                              SizedBox(
                                height: 74,
                              ),
                              AppMainInfoWidget(),
                              SizedBox(
                                height: 61,
                              ),
                            ],
                          )
                        : const SizedBox(
                            height: 55,
                          ),
                    Padding(
                      padding: const EdgeInsets.symmetric(horizontal: 16),
                      child: Row(
                        crossAxisAlignment: CrossAxisAlignment.end,
                        mainAxisAlignment: MainAxisAlignment.spaceBetween,
                        children: [
                          // authorization
                          Text(
                            AppStrings.authHeader,
                            style: AppTextStyles.authHeaderTextStyle,
                          ),

                          // create account
                          InkWell(
                            splashColor: Colors.white,
                            borderRadius: BorderRadius.circular(10),
                            onTap: () {},
                            child: Text(
                              AppStrings.createAccountText,
                              textAlign: TextAlign.center,
                              style: AppTextStyles.createAccountBtnTextStyle,
                            ),
                          ),
                        ],
                      ),
                    ),

                    // tab bar
                    Padding(
                      padding: const EdgeInsets.symmetric(
                        horizontal: 16,
                        vertical: 8,
                      ),
                      child: TabBar(
                        controller: _tabController,
                        onTap: (value) {
                          setState(() {
                            isEmailTab = (value == 0);
                          });
                        },
                        tabs: [
                          SizedBox(
                            height: 48,
                            child: Center(
                              child: Text(AppStrings.emailText),
                            ),
                          ),
                          SizedBox(
                            height: 48,
                            child: Center(
                              child: Text(AppStrings.phoneText),
                            ),
                          ),
                        ],
                      ),
                    ),

                    isEmailTab ? EmailView() : const PhoneView(),
                  ],
                ),
              ),
            ],
          );
        },
      ),
    );
  }
}
