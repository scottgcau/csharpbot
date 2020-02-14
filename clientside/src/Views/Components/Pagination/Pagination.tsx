/*
 * @bot-written
 * 
 * WARNING AND NOTICE
 * Any access, download, storage, and/or use of this source code is subject to the terms and conditions of the
 * Full Software Licence as accepted by you before being granted access to this source code and other materials,
 * the terms of which can be accessed on the Codebots website at https://codebots.com/full-software-licence. Any
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
import { Button, Display } from '../Button/Button';
import { TextField } from '../TextBox/TextBox';
import { observer } from 'mobx-react';
import { observable } from 'mobx';
import IPaginationData from 'Models/PaginationData';

export interface IPaginationProps {
	pagination: IPaginationData;
	showGoToPageBox?: boolean;
	onPageChange?: () => void;
}

enum validPageOptions {
	HIGH,
	VALID,
	LOW,
}

@observer
class Pagination extends React.Component<IPaginationProps> {
	@observable
	private pageTextBoxModel = { page: '1' };

	public render() {
		const { queryOptions } = this.props.pagination;
		const { page } = queryOptions;

		return (
			<nav aria-label="pagination pagination__collections">
				<ul className="collection__pagination">
					<li>{this.renderFirstButton()}</li>
					<li>{this.renderPreviousButton()}</li>
					<li>
						<span className="pagination__page-number">{page + 1} of {this.totalPages()}</span>
					</li>
					<li>{this.renderNextButton()}</li>
					<li>{this.renderLastButton()}</li>
				</ul>
			</nav >
		);
	}

	public renderFirstButton() {
		const { page } = this.props.pagination.queryOptions;
		let isFirstPage = (page === 0);
		return (
			<Button
				disabled={isFirstPage}
				onClick={this.firstPage}
				display={Display.Text}
				labelVisible={false}
				icon={{ icon: "chevrons-left", iconPos: 'icon-left' }}
			>
				First
			</Button>
		);
	}

	public renderNextButton() {
		const { page } = this.props.pagination.queryOptions;
		const noNextPage = (page >= ((this.totalPages()) - 1));
		return (
			<Button
				onClick={this.nextPage}
				display={Display.Text}
				disabled={noNextPage}
				labelVisible={false}
				icon={{ icon: "chevron-right", iconPos: 'icon-right' }}
			>
				Next
			</Button>
		);
	}

	public renderPreviousButton() {
		const { page } = this.props.pagination.queryOptions;
		const noPreviousPage = (page < 1);
		return (
			<Button
				onClick={this.previousPage}
				display={Display.Text}
				disabled={noPreviousPage}
				labelVisible={false}
				icon={{ icon: "chevron-left", iconPos: 'icon-left' }}
			>
				Previous
			</Button>
		);
	}

	public renderLastButton() {
		const { page } = this.props.pagination.queryOptions;
		const isLastPage = (page >= ((this.totalPages()) - 1));
		return (
			<Button
				onClick={this.lastPage}
				display={Display.Text}
				disabled={isLastPage}
				labelVisible={false}
				icon={{ icon: "chevrons-right", iconPos: 'icon-right' }}
			>
				Last
			</Button>
		);
	}

	public renderGoToPageBox() {
		if (this.props.showGoToPageBox) {
			return (
				<form className="paginator__go-to-pg" onSubmit={this.onGoToPageFormSubmit}>
					<TextField model={this.pageTextBoxModel} modelProperty="page" label="Go to page" inputProps={{ type: 'number' }} />
				</form>
			);
		}

		return null;
	}

	private firstPage = () => {
		this.gotoPage(0);
	}

	private previousPage = () => {
		const { queryOptions } = this.props.pagination;
		this.gotoPage(queryOptions.page - 1);
	}

	private nextPage = () => {
		const { queryOptions } = this.props.pagination;
		this.gotoPage(queryOptions.page + 1);
	}

	private lastPage = () => {
		this.gotoPage(this.totalPages() - 1);
	}

	private onGoToPageFormSubmit = (event: React.FormEvent<HTMLFormElement>) => {
		event.preventDefault();

		const n = Number.parseInt(this.pageTextBoxModel.page, 10);
		if (!isNaN(n)) {
			this.gotoPage(n - 1);
		}
	}

	public gotoPage = (pageNo: number) => {
		const { queryOptions } = this.props.pagination;

		const validPage = this.isValidPage(pageNo);

		if (validPage === validPageOptions.HIGH) {
			queryOptions.gotoPage(this.totalPages() - 1);
		} else if (validPage === validPageOptions.LOW) {
			queryOptions.gotoPage(0);
		}

		queryOptions.gotoPage(pageNo);

		if (this.props.onPageChange) {
			this.props.onPageChange();
		}
	}

	private isValidPage = (pageNo: number): validPageOptions => {
		if (pageNo >= this.totalPages()) {
			return validPageOptions.HIGH;
		} else if (pageNo < 0) {
			return validPageOptions.LOW;
		}
		return validPageOptions.VALID;
	}

	private totalPages () {
		const { queryOptions, totalRecords } = this.props.pagination;
		const { perPage } = queryOptions;

		if (totalRecords > 0) {
			return Math.ceil(totalRecords / perPage);
		}
		return 1;
	}
}

export default Pagination;