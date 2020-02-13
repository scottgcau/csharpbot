/*
 * @bot-written
 * 
 * WARNING AND NOTICE
 * Any access, download, storage, and/or use of this source code is subject to the terms and conditions of the
 * Full Software Licence as accepted by you before being granted access to this source code and other materials,
 * the terms of which can be accessed on the Codebots website at https://codebots.com/full-software-license. Any
 * commercial use in contravention of the terms of the Full Software Licence may be pursued by Codebots through
 * licence termination and further legal action, and be required to indemnify Codebots for any loss or damage,
 * including interest and costs. You are deemed to have accepted the terms of the Full Software Licence on any
 * access, download, storage, and/or use of this source code.
 * 
 * BOT WARNING
 * This file is bot-written.
 * Any changes out side of "protected regions" will be lost next time the bot makes any changes.
 */
import * as React from 'react';
import { store } from '../../../Models/Store';
import { Button, Display } from '../Button/Button';

export interface IConfirmModalOptions {
	/** The text to be placed in the confirm button */
	confirmText?: string;
	/** The text to be placed in the cancel button */
	cancelText?: string
}

/**
 * Opens a confirm modal which gives a user an option to confirm or cancel
 * @param title The title on the modal
 * @param message The message to display in the modal
 * @param options Extra options for the modal
 * @returns A promise that is resolved on the user confirming or rejected on the user cancelling
 */
export function confirmModal(title: string, message: React.ReactNode, options: IConfirmModalOptions = {}) {
	return new Promise((resolve, reject) => {
		const onConfirm = () => {
			store.modal.hide();
			resolve();
		};

		const onCancel = () => {
			store.modal.hide();
			reject();
		};

		const confirmText = options.confirmText || 'Confirm';
		const cancelText = options.cancelText || 'Cancel';

		const confirmDom = (
			<>
				<div key="header" className="modal__header">
					<h2 key="header-item" className="modal__title">{title}</h2>
					<Button
						key="header-cancel"
						icon={{icon: 'square-x', iconPos: 'icon-left'}}
						display={Display.Text}
						onClick={onCancel}
						labelVisible={false}>
						{cancelText}
					</Button>
				</div>
				<div key="message" className="modal__message">
					{message}
				</div>
				<div key="actions" className="modal__actions">
					<Button key={"confirm"} onClick={onConfirm} display={Display.Solid}>{confirmText}</Button>
					<Button key={"cancel"} onClick={onCancel} display={Display.Outline}>{cancelText}</Button>
				</div>
			</>
		);

		store.modal.show(title, confirmDom, {
			className: 'confirm-modal',
			onRequestClose: () => {
				store.modal.hide();
				reject();
			},
		});
	});
}

export interface IAlertModalProps {
	/** The text to be placed in the cancel button */
	cancelText?: string;
}

/**
 * Displays an alert on the screen
 * @param title A title for the modal
 * @param message The message content for the modal
 * @param options Extra options for the modal
 * @returns A promise that is resolved when the modal is closed
 */
export function alertModal(title: string, message: React.ReactNode, options: IAlertModalProps = {}) {
	return new Promise((resolve) => {
		const onClose = () => {
			store.modal.hide();
			resolve();
		};

		const cancelText = options.cancelText || 'Cancel';

		const alertDom = (
			<>
				<div key="header" className="modal__header">
					<h2 key="header-item" className="modal__title">{title}</h2>
					<Button
						key="header-cancel"
						icon={{icon: 'square-x', iconPos: 'icon-left'}}
						display={Display.Text}
						onClick={onClose}
						labelVisible={false}>
						{cancelText}
					</Button>
				</div>
				<div key="message" className="modal__message">
					{message}
				</div>
			</>
		);

		store.modal.show(title, alertDom, {
			className: 'alert-modal',
			onRequestClose: () => {
				store.modal.hide();
				resolve();
			},
		});
	});
}