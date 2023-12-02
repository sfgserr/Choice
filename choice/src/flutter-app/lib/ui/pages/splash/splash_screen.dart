import 'splash_widgets.dart';
import 'package:flutter/material.dart';

class SplashScreen extends StatelessWidget {
  const SplashScreen({super.key});

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


