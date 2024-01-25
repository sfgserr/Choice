import 'dart:async';
import 'package:choice/features/entry_point/bloc/auth_bloc.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'splash_widgets.dart';
import 'package:flutter/material.dart';

class SplashScreen extends StatefulWidget {
  const SplashScreen({super.key});

  @override
  State<SplashScreen> createState() => _SplashScreenState();
}

class _SplashScreenState extends State<SplashScreen> {

  @override
  void initState() {
    super.initState();
    // make a 3-seconds pause before the app start
    Timer(
      const Duration(seconds: 3),
          () => BlocProvider.of<AuthBloc>(context).add(AppStarted()),
    );
  }

  @override
  Widget build(BuildContext context) {
    return const Scaffold(
      body: CustomScrollView(
        slivers: [
          SliverToBoxAdapter(
            child: Column(
              children: [
                SizedBox(height: 262,),
                AppMainInfoWidget(),
                Padding(
                  padding: EdgeInsets.fromLTRB(66, 192, 66, 138),
                  child: LinearProgressIndicator(),
                ),
              ],
            ),
          ),
        ],
      )
    );
  }
}


