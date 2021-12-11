import { PalestranteEvento } from "./PalestranteEvento";
import { RedeSocial } from "./RedeSocial";

export interface Palestrante {
 Id: number
 Nome: string
 MineCurriculo: string
 ImagemURL: string
 Telefone: string
 Email: string
 RedesSociais: RedeSocial[];
 PalestrantesEventos: PalestranteEvento[];
}
