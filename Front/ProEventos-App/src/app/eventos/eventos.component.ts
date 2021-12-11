import { Component, OnInit, TemplateRef } from '@angular/core';

import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

import { EventoService } from 'src/services/evento.service';
import { Evento } from '../models/Evento';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss']
})
export class EventosComponent implements OnInit {

  public eventos: Evento[] = [];
  public eventosFiltrados: Evento[] = [];
  public widthImg: number = 100;
  public marginImg: number = 2;
  public showImg: boolean = false;
  private _filtroLista: string = "";
  private modalRef?: BsModalRef;

  public get filtroLista(): string{
    return this._filtroLista;
  }
  public set filtroLista(value: string){
    this._filtroLista = value;
    this.eventosFiltrados = this.filtroLista ? this.filtrarEventos(this.filtroLista) : this.eventos;
  }

  public filtrarEventos(filtrarPor: string): Evento[]{
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.eventos.filter(
    evento => evento.tema.toLocaleLowerCase().indexOf(filtrarPor) !== -1 ||
      evento.local.toLocaleLowerCase().indexOf(filtrarPor) !== -1

    );

  }

  constructor(private eventoService: EventoService,
              private modalService: BsModalService,
              private toastr: ToastrService,
              private spinner: NgxSpinnerService ) { }

  public ngOnInit(): void {
    this.spinner.show();
    this.getEventos();

  }

  public changeImg(): void{
    this.showImg = !this.showImg;
    console.log(this.showImg)
  }

  public getEventos(): void{
    this.eventoService.getEventos().subscribe({
     next: (_eventos: Evento[]) => {
        this.eventos = _eventos;
        this.eventosFiltrados = this.eventos;
      },
     error: (error: any) => {
       this.spinner.hide();
       this.toastr.error('Erro ao Carregar os Eventos', 'Error!')
     },
     complete: () => this.spinner.hide()
    });
  }
  openModal(template: TemplateRef<any> ) : void {
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  confirm(): void {
    this.modalRef?.hide();
    this.toastr.success('O Evento foi deletado com Sucesso.', 'Deletado!');
  }

  decline(): void {
    this.modalRef?.hide();
  }

}
