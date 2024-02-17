part of 'card_company_bloc.dart';

abstract class CardCompanyEvent extends Equatable {
  const CardCompanyEvent();

  @override
  List<Object> get props => [];
}

class ChangeCardCompanyView extends CardCompanyEvent {
  final int newPage;

  const ChangeCardCompanyView({required this.newPage});

  @override
  List<Object> get props => [newPage];
}

class EnableCardCompanyMainBtn extends CardCompanyEvent {
  final bool isMainBtnEnabled;

  const EnableCardCompanyMainBtn({required this.isMainBtnEnabled});

  @override
  List<Object> get props => [isMainBtnEnabled];
}