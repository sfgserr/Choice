import 'package:choice/features/auth/company_card/bloc/card_company_bloc.dart';
import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'views/views.dart';

class CurrentView extends StatelessWidget {
  const CurrentView({
    super.key,
    required this.scrollController,
  });

  final ScrollController scrollController;

  @override
  Widget build(BuildContext context) {
    BlocBuilder<CardCompanyBloc, CardCompanyState>(
      builder: (context, state) {
        print(state);

        return const SizedBox();
      },
    );

    return const SizedBox();
  }
}
